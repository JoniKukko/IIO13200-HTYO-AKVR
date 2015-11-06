using System;

public partial class TEST : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // just a simple test to test this test
        var TrainService = AKVR.Services.Factory.Train();
        var Train = TrainService.Select(70561);
        result.Text = Train.trainCategory + " " + Train.trainType + " " + Train.trainNumber.ToString();
    }
}