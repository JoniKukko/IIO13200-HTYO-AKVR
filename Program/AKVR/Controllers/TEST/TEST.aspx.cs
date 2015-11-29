using System;
using AKVR.Services;
using System.Linq;
using System.Diagnostics;

public partial class TEST : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        var trainService = ServiceFactory.Train();
        var trainList = trainService.SelectDelaysBetweenDates(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1));
        
        result.Text = "";
        
        foreach (var train in trainList)
        {
            result.Text += train.FullTrainName
                + " <br>"
                + "- Average: " + train.AverageDelay + "<br>"
                + "- Max: " + train.MaxDelay + "<br>"
                + "<br>";
        }

        
        
    }


}