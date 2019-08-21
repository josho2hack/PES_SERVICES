using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Timers;

namespace Point01Service
{
    public partial class Service1 : ServiceBase
    {
        private readonly EPESEntities _context;

        Timer timer = new Timer(); // name space(using System.Timers;)  
        public Service1(EPESEntities context)
        {
            InitializeComponent();
            _context = context;
        }

        public Service1()
        {
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += OnTimedEvent;
            timer.Interval = 86400000; //(24 Hour) number in milisecinds  60000 = 1 Min.
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            //if (DateTime.Now.Day == 15)
            //{
                GetDataPoint01();
            //}
        }

        private async void GetDataPoint01()
        {
            var office = _context.Offices.Where(d => d.Code != "00000000" && d.Code.Substring(5, 3) == "000");

            DateTime yearForRequest;
            if (DateTime.Now.Month == 10 || DateTime.Now.Month == 11 || DateTime.Now.Month == 12)
            {
                yearForRequest = new DateTime(DateTime.Now.AddYears(1).Year, 1, 1);
            }
            else
            {
                yearForRequest = new DateTime(DateTime.Now.Year, 1, 1);
            }

            foreach (var item in office)
            {
                //DataForEvaluations dataForEvaluation = null;
                //string url = "";

                if (item.Code == "00009000" || (item.Code.Substring(5, 3) == "000" && item.Code.Substring(0, 3) != "000"))
                {
                    var m = DateTime.Now.AddMonths(-1).Month;
                    if (m == 10 || m == 11 || m == 12)
                    {
                        for (int i = 10; i <= m; i++)
                        {
                            var dataForEvaluation = await _context.DataForEvaluations.Where(d => d.Offices.Code == "00009000" && d.Month == i && d.PointOfEvaluations.Year == yearForRequest && d.PointOfEvaluations.Name.Contains("ผลการจัดเก็บภาษี") && d.PointOfEvaluations.Unit == 0).Include(d => d.PointOfEvaluations).FirstOrDefaultAsync();

                            var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + item.Code + "/year/" + yearForRequest.Year.ToString("D4", CultureInfo.CreateSpecificCulture("th-TH")) + "/month/" + m.ToString("D2");

                            var tax = _download_serialized_json_data<RootObject>(url);
                            var taxOwn = tax.TaxCol.FirstOrDefault(t => t.officeCode == item.Code);
                            if (taxOwn != null && dataForEvaluation?.Result == 0)
                            {
                                dataForEvaluation.Expect = taxOwn.CMCYforcast;
                                dataForEvaluation.Result = taxOwn.CMcurrentYear;
                                dataForEvaluation.Approve = 1;
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    else
                    {
                        for (int i = 10; i <= 12; i++)
                        {
                            var dataForEvaluation = await _context.DataForEvaluations.Where(d => d.Offices.Code == "00009000" && d.Month == i && d.PointOfEvaluations.Year == yearForRequest && d.PointOfEvaluations.Name.Contains("ผลการจัดเก็บภาษี") && d.PointOfEvaluations.Unit == 0).Include(d => d.PointOfEvaluations).FirstOrDefaultAsync();

                            var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + item.Code + "/year/" + yearForRequest.Year.ToString("D4", CultureInfo.CreateSpecificCulture("th-TH")) + "/month/" + m.ToString("D2");

                            var tax = _download_serialized_json_data<RootObject>(url);
                            var taxOwn = tax.TaxCol.FirstOrDefault(t => t.officeCode == item.Code);
                            if (taxOwn != null && dataForEvaluation?.Result == 0)
                            {
                                dataForEvaluation.Expect = taxOwn.CMCYforcast;
                                dataForEvaluation.Result = taxOwn.CMcurrentYear;
                                dataForEvaluation.Approve = 1;
                                await _context.SaveChangesAsync();
                            }
                        }

                        for (int i = 1; i <= m; i++)
                        {
                            var dataForEvaluation = await _context.DataForEvaluations.Where(d => d.Offices.Code == "00009000" && d.Month == i && d.PointOfEvaluations.Year == yearForRequest && d.PointOfEvaluations.Name.Contains("ผลการจัดเก็บภาษี") && d.PointOfEvaluations.Unit == 0).Include(d => d.PointOfEvaluations).FirstOrDefaultAsync();

                            var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + item.Code + "/year/" + yearForRequest.Year.ToString("D4", CultureInfo.CreateSpecificCulture("th-TH")) + "/month/" + m.ToString("D2");

                            var tax = _download_serialized_json_data<RootObject>(url);
                            var taxOwn = tax.TaxCol.FirstOrDefault(t => t.officeCode == item.Code);
                            if (taxOwn != null && dataForEvaluation?.Result == 0)
                            {
                                dataForEvaluation.Expect = taxOwn.CMCYforcast;
                                dataForEvaluation.Result = taxOwn.CMcurrentYear;
                                dataForEvaluation.Approve = 1;
                                await _context.SaveChangesAsync();
                            }
                        }
                    }  
                }
            }
        }

        private static T _download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }
    }
}
