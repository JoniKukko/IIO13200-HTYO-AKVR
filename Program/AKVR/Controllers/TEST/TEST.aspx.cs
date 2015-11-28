using System;
using AKVR.Services;
using System.Linq;

public partial class TEST : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        var trainService = ServiceFactory.Train();
        var trainTypeService = ServiceFactory.TrainType();
        
        var trainModel = trainService.SelectByTrainNumber(1);

        var trainTypeList = trainTypeService.SelectAll();
        var trainTypes = trainTypeList.ToDictionary(x => x.name, x => x.trainCategory.name);
        
        result.Text = trainTypes[trainModel.trainType] + " " + trainModel.trainType + " " + trainModel.trainNumber + " " + trainModel.trainCategory;
        
    }


}