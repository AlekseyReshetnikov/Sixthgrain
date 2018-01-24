using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grain;
using Grain.Controllers;
using Moq;
using Grain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Grain.Tests.Controllers
{
    [TestClass]
    public class FarmsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            Mock<IGrainRepository> mock = new Mock<IGrainRepository>();
            FarmsController controller = new FarmsController(mock.Object);
            // Act
            Task<ActionResult> taskResult = controller.Index();
            taskResult.Wait();
            ActionResult result = taskResult.Result as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Pivot()
        {
            // Arrange
            Mock<IGrainRepository> mock = new Mock<IGrainRepository>();
            PivotController controller = new PivotController(mock.Object);
            // Act
            Task<ActionResult> taskResult = controller.Index(1,2,3);
            taskResult.Wait();
            ActionResult result = taskResult.Result as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Pivot_rainContext()
        {
            var db = new GrainContext();
            string s = db.Agricultures.Find(1).Name;

            Task<PivotView> TaskModel = PivotContext.GeneratePivotViewModel(db, 1, 2, 4);
            TaskModel.Wait();
            PivotView Model = TaskModel.Result;
            Assert.IsNotNull(Model.Columns);
            Assert.IsNotNull(Model.Rows);
            Assert.IsTrue(Model.ColumnsCount>0);
        }

        [TestMethod]
        public void FilledData()
        {
            var db = new GrainContext();
            Assert.IsTrue(db.Agricultures.Count() > 3);
            Assert.IsTrue(db.Regions.Count() > 3);
            Assert.IsTrue(db.Farms.Count() > 3);
        }

        [TestMethod]
        public void Can_Create_ValidFarm()
        {
            //Arrange
            Mock<IGrainRepository> mock = new Mock<IGrainRepository>();
            FarmsController controller = new FarmsController(mock.Object);

            Farm farm = new Farm
            {
                //Id = 1,
                Name = "Test",
                FarmerName = "Farmer",
                HarvestLastYear = 10,
                Area = 5,
             //   AgricultureId = 1,
                RegionId = 1
            };


            //Act
            ActionResult result = Await(controller.Create(farm));

            //Assert
            mock.Verify(m => m.SaveFarmAsync(farm));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Create_InvalidFarm()
        {
            //Arrange
            Mock<IGrainRepository> mock = new Mock<IGrainRepository>();
            FarmsController controller = new FarmsController(mock.Object);
            Farm farm = new Farm
            {
                //Id = 1,
                Name = "Test",
                FarmerName = "Farmer",
                HarvestLastYear = 10,
                Area = 5,
                //   AgricultureId = 1,
                RegionId = 1
            };
            
            controller.ModelState.AddModelError("error", "error");

            //Act
            ActionResult result = Await(controller.Create(farm));

            //Assert
            mock.Verify(m => m.SaveFarmAsync(farm), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Farms()
        {
            //Arrange
            Mock<IGrainRepository> mock = new Mock<IGrainRepository>();
            FarmsController controller = new FarmsController(mock.Object);

            //Act
            Await(controller.Delete(2));
            //Assert
            mock.Verify(m => m.FarmsFindAsync(2));
        }

        ActionResult Await(Task<ActionResult> tresult)
        {
            tresult.Wait();
            return tresult.Result;
        }
    }
}