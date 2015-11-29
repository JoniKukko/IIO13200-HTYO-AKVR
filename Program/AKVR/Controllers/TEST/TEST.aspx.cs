using System;
using AKVR.Services;
using System.Linq;
using System.Diagnostics;

public partial class TEST : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        var trainService = ServiceFactory.Train();
        var trainList = trainService.SelectCausesByDate(DateTime.Now.AddDays(-1));

        var reasonCategoryService = ServiceFactory.ReasonCategory();
        var reasonCategoryList = reasonCategoryService.SelectAll();
        var reasonCategorys = reasonCategoryList.ToDictionary(x => x.categoryCode.ToString(), x => x.categoryName.ToString());

        var reasonCodeService = ServiceFactory.ReasonCode();
        var reasonCodeList = reasonCodeService.SelectAll();
        var reasonCodes = reasonCodeList.ToDictionary(x => x.detailedCategoryCode.ToString(), x => x.detailedCategoryName.ToString());

        var trafficLocationService = ServiceFactory.TrafficLocation();
        var trafficLocationList = trafficLocationService.SelectAll();
        var trafficLocations = trafficLocationList.ToDictionary(x => x.stationShortCode, x => x.stationName);



        result.Text = "";

        foreach (var train in trainList)
        {
            result.Text += train.FullTrainName + "<br>";
            foreach (var row in train.timeTableRows)
            {
                result.Text += "- " + trafficLocations[row.stationShortCode] + " ";
                foreach (var cause in row.causes)
                {
                    result.Text += "(" 
                        + (cause.categoryCode != null ? reasonCategorys[cause.categoryCode] : null)
                        + ": "
                        + (cause.detailedCategoryCode != null ? reasonCodes[cause.detailedCategoryCode] : null)
                        + ") ";
                }
                result.Text += "<br>";
            }

        }



        
        
    }


}