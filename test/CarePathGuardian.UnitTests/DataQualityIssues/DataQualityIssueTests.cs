using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.UnitTests.DataQualityIssues
{
    public class DataQualityIssueTests
    {
        [Fact]
		public void Constructor_WhenDataIsValid_ShouldCreateDataQualityIssue()
		{
			Guid entityId = Guid.NewGuid();
			DataQualityIssue dataQualityIssue = new DataQualityIssue(
				"Type 10", entityId, "ruleCode",
				"description", IssueSeverity.High);

				Assert.NotEqual(Guid.Empty,dataQualityIssue.Id);
				Assert.Equal("Type 10", dataQualityIssue.EntityType);
				Assert.Equal(entityId, dataQualityIssue.EntityId);
				Assert.Equal("ruleCode", dataQualityIssue.RuleCode);
				Assert.Equal("description", dataQualityIssue.Description);
				Assert.Equal(IssueSeverity.High, dataQualityIssue.Severity);
				Assert.Equal(IssueStatus.Open, dataQualityIssue.Status);
				Assert.Null(dataQualityIssue.ResolvedAtUtc);
				Assert.Null(dataQualityIssue.ResolutionNotes);
				Assert.True(dataQualityIssue.DetectedAtUtc <= DateTime.UtcNow);    
		}
		[Fact]
		public void Constructor_WhenEntityTypeIsEmpty_ShouldThrowArgumentException()
		{
			Guid entityId = Guid.NewGuid();
			Action action = () => new DataQualityIssue("", entityId,"ruleCode","description", IssueSeverity.High);
			var exception = Assert.Throws<ArgumentException>(action);
			Assert.Equal("entityType", exception.ParamName);
		}
		
		[Fact]
		public void Constructor_WhenEntityIdIsEmpty_ShouldThrowArgumentException()
		{
			Guid entityId = Guid.Empty;
			Action action = () => new DataQualityIssue("Type 10", entityId,"ruleCode","description", IssueSeverity.High);
			var exception = Assert.Throws<ArgumentException>(action);
			Assert.Equal("entityId", exception.ParamName);
		}
		
		[Fact]
		public void Constructor_WhenRuleCodeIsEmpty_ShouldThrowArgumentException()
		{
			Guid entityId = Guid.NewGuid();
			Action action = () => new DataQualityIssue("Type 10", entityId,"","description", IssueSeverity.High);
			var exception = Assert.Throws<ArgumentException>(action);
			Assert.Equal("ruleCode", exception.ParamName);			
		}
		
		
		[Fact]
		public void Constructor_WhenDescriptionIsEmpty_ShouldThrowArgumentException()
		{
			Guid entityId = Guid.NewGuid();
			Action action = () => new DataQualityIssue("Type 10", entityId,"ruleCode","", IssueSeverity.High);
			var exception = Assert.Throws<ArgumentException>(action);
			Assert.Equal("description", exception.ParamName);
		}
    }

}