using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MissionControlDbTests
{
  [TestClass()]
  public class CarryOutTests : SqlDatabaseTestClass
  {

    public CarryOutTests()
    {
      InitializeComponent();
    }

    [TestInitialize()]
    public void TestInitialize()
    {
      base.InitializeTest();
    }
    [TestCleanup()]
    public void TestCleanup()
    {
      base.CleanupTest();
    }

    [TestMethod()]
    public void CanVerifyCapeCanaveralMenu()
    {
      SqlDatabaseTestActions testActions = this.CanVerifyCapeCanaveralMenuData;
      // Execute the pre-test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
      SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
      // Execute the test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
      SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
      // Execute the post-test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
      SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
    }
    [TestMethod()]
    public void CanFindJellyDonut()
    {
      SqlDatabaseTestActions testActions = this.CanFindJellyDonutData;
      // Execute the pre-test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
      SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
      try
      {
        // Execute the test script
        // 
        System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
        SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
      }
      finally
      {
        // Execute the post-test script
        // 
        System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
        SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
      }
    }
    [TestMethod()]
    public void CannotFindJellyDonutInBackyard()
    {
      SqlDatabaseTestActions testActions = this.CannotFindJellyDonutInBackyardData;
      // Execute the pre-test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
      SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
      try
      {
        // Execute the test script
        // 
        System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
        SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
      }
      finally
      {
        // Execute the post-test script
        // 
        System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
        SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
      }
    }
    [TestMethod()]
    public void CanFindBurritoAtJohnsonSpaceCenterOnly()
    {
      SqlDatabaseTestActions testActions = this.CanFindBurritoAtJohnsonSpaceCenterOnlyData;
      // Execute the pre-test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
      SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
      try
      {
        // Execute the test script
        // 
        System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
        SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
      }
      finally
      {
        // Execute the post-test script
        // 
        System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
        SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
      }
    }
    [TestMethod()]
    public void CanGetMilitaryDiscountOnPizza()
    {
      SqlDatabaseTestActions testActions = this.CanGetMilitaryDiscountOnPizzaData;
      // Execute the pre-test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
      SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
      try
      {
        // Execute the test script
        // 
        System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
        SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
      }
      finally
      {
        // Execute the post-test script
        // 
        System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
        SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
      }
    }





    #region Designer support code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction CanVerifyCapeCanaveralMenu_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarryOutTests));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition mustHaveResults;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition mustHaveCorrectRow1Id;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition mustHaveCorrectRow2Id;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition mustHaveCorrectRow3Id;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction CanFindJellyDonut_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition notEmptyJellyDonut;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction CannotFindJellyDonutInBackyard_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition emptyJellyDonutResultsInBackyard;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction CanFindBurritoAtJohnsonSpaceCenterOnly_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition noBurritoInBackyard;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition noBurritoInFlorida;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition noBurritoInRussia;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition foundBurritoInTexas;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction CanGetMilitaryDiscountOnPizza_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition pizzaCount;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition isReallyPizza;
            this.CanVerifyCapeCanaveralMenuData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.CanFindJellyDonutData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.CannotFindJellyDonutInBackyardData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.CanFindBurritoAtJohnsonSpaceCenterOnlyData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.CanGetMilitaryDiscountOnPizzaData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            CanVerifyCapeCanaveralMenu_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            mustHaveResults = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            mustHaveCorrectRow1Id = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            mustHaveCorrectRow2Id = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            mustHaveCorrectRow3Id = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            CanFindJellyDonut_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            notEmptyJellyDonut = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            CannotFindJellyDonutInBackyard_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            emptyJellyDonutResultsInBackyard = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition();
            CanFindBurritoAtJohnsonSpaceCenterOnly_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            noBurritoInBackyard = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition();
            noBurritoInFlorida = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition();
            noBurritoInRussia = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition();
            foundBurritoInTexas = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            CanGetMilitaryDiscountOnPizza_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            pizzaCount = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            isReallyPizza = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            // 
            // CanVerifyCapeCanaveralMenu_TestAction
            // 
            CanVerifyCapeCanaveralMenu_TestAction.Conditions.Add(mustHaveResults);
            CanVerifyCapeCanaveralMenu_TestAction.Conditions.Add(mustHaveCorrectRow1Id);
            CanVerifyCapeCanaveralMenu_TestAction.Conditions.Add(mustHaveCorrectRow2Id);
            CanVerifyCapeCanaveralMenu_TestAction.Conditions.Add(mustHaveCorrectRow3Id);
            resources.ApplyResources(CanVerifyCapeCanaveralMenu_TestAction, "CanVerifyCapeCanaveralMenu_TestAction");
            // 
            // mustHaveResults
            // 
            mustHaveResults.Enabled = true;
            mustHaveResults.Name = "mustHaveResults";
            mustHaveResults.ResultSet = 1;
            // 
            // mustHaveCorrectRow1Id
            // 
            mustHaveCorrectRow1Id.ColumnNumber = 1;
            mustHaveCorrectRow1Id.Enabled = true;
            mustHaveCorrectRow1Id.ExpectedValue = "10000";
            mustHaveCorrectRow1Id.Name = "mustHaveCorrectRow1Id";
            mustHaveCorrectRow1Id.NullExpected = false;
            mustHaveCorrectRow1Id.ResultSet = 1;
            mustHaveCorrectRow1Id.RowNumber = 1;
            // 
            // mustHaveCorrectRow2Id
            // 
            mustHaveCorrectRow2Id.ColumnNumber = 1;
            mustHaveCorrectRow2Id.Enabled = true;
            mustHaveCorrectRow2Id.ExpectedValue = "10001";
            mustHaveCorrectRow2Id.Name = "mustHaveCorrectRow2Id";
            mustHaveCorrectRow2Id.NullExpected = false;
            mustHaveCorrectRow2Id.ResultSet = 1;
            mustHaveCorrectRow2Id.RowNumber = 2;
            // 
            // mustHaveCorrectRow3Id
            // 
            mustHaveCorrectRow3Id.ColumnNumber = 1;
            mustHaveCorrectRow3Id.Enabled = true;
            mustHaveCorrectRow3Id.ExpectedValue = "10002";
            mustHaveCorrectRow3Id.Name = "mustHaveCorrectRow3Id";
            mustHaveCorrectRow3Id.NullExpected = false;
            mustHaveCorrectRow3Id.ResultSet = 1;
            mustHaveCorrectRow3Id.RowNumber = 3;
            // 
            // CanFindJellyDonut_TestAction
            // 
            CanFindJellyDonut_TestAction.Conditions.Add(notEmptyJellyDonut);
            resources.ApplyResources(CanFindJellyDonut_TestAction, "CanFindJellyDonut_TestAction");
            // 
            // notEmptyJellyDonut
            // 
            notEmptyJellyDonut.Enabled = true;
            notEmptyJellyDonut.Name = "notEmptyJellyDonut";
            notEmptyJellyDonut.ResultSet = 1;
            // 
            // CannotFindJellyDonutInBackyard_TestAction
            // 
            CannotFindJellyDonutInBackyard_TestAction.Conditions.Add(emptyJellyDonutResultsInBackyard);
            resources.ApplyResources(CannotFindJellyDonutInBackyard_TestAction, "CannotFindJellyDonutInBackyard_TestAction");
            // 
            // emptyJellyDonutResultsInBackyard
            // 
            emptyJellyDonutResultsInBackyard.Enabled = true;
            emptyJellyDonutResultsInBackyard.Name = "emptyJellyDonutResultsInBackyard";
            emptyJellyDonutResultsInBackyard.ResultSet = 1;
            // 
            // CanFindBurritoAtJohnsonSpaceCenterOnly_TestAction
            // 
            CanFindBurritoAtJohnsonSpaceCenterOnly_TestAction.Conditions.Add(noBurritoInBackyard);
            CanFindBurritoAtJohnsonSpaceCenterOnly_TestAction.Conditions.Add(noBurritoInFlorida);
            CanFindBurritoAtJohnsonSpaceCenterOnly_TestAction.Conditions.Add(noBurritoInRussia);
            CanFindBurritoAtJohnsonSpaceCenterOnly_TestAction.Conditions.Add(foundBurritoInTexas);
            resources.ApplyResources(CanFindBurritoAtJohnsonSpaceCenterOnly_TestAction, "CanFindBurritoAtJohnsonSpaceCenterOnly_TestAction");
            // 
            // noBurritoInBackyard
            // 
            noBurritoInBackyard.Enabled = true;
            noBurritoInBackyard.Name = "noBurritoInBackyard";
            noBurritoInBackyard.ResultSet = 1;
            // 
            // noBurritoInFlorida
            // 
            noBurritoInFlorida.Enabled = true;
            noBurritoInFlorida.Name = "noBurritoInFlorida";
            noBurritoInFlorida.ResultSet = 2;
            // 
            // noBurritoInRussia
            // 
            noBurritoInRussia.Enabled = true;
            noBurritoInRussia.Name = "noBurritoInRussia";
            noBurritoInRussia.ResultSet = 3;
            // 
            // foundBurritoInTexas
            // 
            foundBurritoInTexas.Enabled = true;
            foundBurritoInTexas.Name = "foundBurritoInTexas";
            foundBurritoInTexas.ResultSet = 4;
            // 
            // CanGetMilitaryDiscountOnPizza_TestAction
            // 
            CanGetMilitaryDiscountOnPizza_TestAction.Conditions.Add(pizzaCount);
            CanGetMilitaryDiscountOnPizza_TestAction.Conditions.Add(isReallyPizza);
            resources.ApplyResources(CanGetMilitaryDiscountOnPizza_TestAction, "CanGetMilitaryDiscountOnPizza_TestAction");
            // 
            // pizzaCount
            // 
            pizzaCount.ColumnNumber = 1;
            pizzaCount.Enabled = true;
            pizzaCount.ExpectedValue = "1";
            pizzaCount.Name = "pizzaCount";
            pizzaCount.NullExpected = false;
            pizzaCount.ResultSet = 1;
            pizzaCount.RowNumber = 1;
            // 
            // isReallyPizza
            // 
            isReallyPizza.ColumnNumber = 2;
            isReallyPizza.Enabled = true;
            isReallyPizza.ExpectedValue = "Pizza";
            isReallyPizza.Name = "isReallyPizza";
            isReallyPizza.NullExpected = false;
            isReallyPizza.ResultSet = 1;
            isReallyPizza.RowNumber = 1;
            // 
            // CanVerifyCapeCanaveralMenuData
            // 
            this.CanVerifyCapeCanaveralMenuData.PosttestAction = null;
            this.CanVerifyCapeCanaveralMenuData.PretestAction = null;
            this.CanVerifyCapeCanaveralMenuData.TestAction = CanVerifyCapeCanaveralMenu_TestAction;
            // 
            // CanFindJellyDonutData
            // 
            this.CanFindJellyDonutData.PosttestAction = null;
            this.CanFindJellyDonutData.PretestAction = null;
            this.CanFindJellyDonutData.TestAction = CanFindJellyDonut_TestAction;
            // 
            // CannotFindJellyDonutInBackyardData
            // 
            this.CannotFindJellyDonutInBackyardData.PosttestAction = null;
            this.CannotFindJellyDonutInBackyardData.PretestAction = null;
            this.CannotFindJellyDonutInBackyardData.TestAction = CannotFindJellyDonutInBackyard_TestAction;
            // 
            // CanFindBurritoAtJohnsonSpaceCenterOnlyData
            // 
            this.CanFindBurritoAtJohnsonSpaceCenterOnlyData.PosttestAction = null;
            this.CanFindBurritoAtJohnsonSpaceCenterOnlyData.PretestAction = null;
            this.CanFindBurritoAtJohnsonSpaceCenterOnlyData.TestAction = CanFindBurritoAtJohnsonSpaceCenterOnly_TestAction;
            // 
            // CanGetMilitaryDiscountOnPizzaData
            // 
            this.CanGetMilitaryDiscountOnPizzaData.PosttestAction = null;
            this.CanGetMilitaryDiscountOnPizzaData.PretestAction = null;
            this.CanGetMilitaryDiscountOnPizzaData.TestAction = CanGetMilitaryDiscountOnPizza_TestAction;
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        private SqlDatabaseTestActions CanVerifyCapeCanaveralMenuData;
    private SqlDatabaseTestActions CanFindJellyDonutData;
    private SqlDatabaseTestActions CannotFindJellyDonutInBackyardData;
    private SqlDatabaseTestActions CanFindBurritoAtJohnsonSpaceCenterOnlyData;
    private SqlDatabaseTestActions CanGetMilitaryDiscountOnPizzaData;
  }
}
