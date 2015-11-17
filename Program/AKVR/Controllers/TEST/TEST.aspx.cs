using System;
using System.Collections.Generic;
using AKVR.Services;
using AKVR.Services.Train;
using AKVR.Services.TrafficLocation;

public partial class TEST : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        var trainService  = ServiceFactory.Train();
        var trainList = trainService.SelectAll();

        result.Text = "";

        foreach (var train in trainList)
        {
            result.Text = result.Text + " " + train.departureDate.ToString("dd.MM.") + " " + train.trainCategory + " " + train.trainType + train.trainNumber + " " + train.timeTableRows[0].stationShortCode + "-" + train.timeTableRows[train.timeTableRows.Length-1].stationShortCode + "<br/>";
        }
        

    }


}