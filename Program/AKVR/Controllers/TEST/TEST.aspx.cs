using System;
using AKVR.Services;
using System.Linq;
using System.Diagnostics;

public partial class TEST : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        var trainService = ServiceFactory.Train();
        var trainList = trainService.SelectAll();

        var stationService = ServiceFactory.TrafficLocation();
        var stationList = stationService.SelectAll();
        var stations = stationList.ToDictionary(x => x.stationShortCode, x => x.stationName);



        trainList = trainList.Select(train => { train.timeTableRows = train.timeTableRows.Select(row => { row.stationName = stations[row.stationShortCode]; return row; }).ToList(); return train; }).ToList();



        foreach (var train in trainList)
        {
            result.Text += train.FullTrainName + "<br>";
            foreach (var row in train.timeTableRows)
            {
                result.Text += "- " + row.stationShortCode + ": " + row.stationName + "<br>";
            }
        }

        
        
    }


}