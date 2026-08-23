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

		[Fact]
		public void Resolve_WhenIssueIsOpen_ShouldResolveIssue()
		{
			Guid entityId = Guid.NewGuid();
			DataQualityIssue dataQualityIssue = new DataQualityIssue(
				"Type 10", entityId, "ruleCode",
				"description", IssueSeverity.High);
			dataQualityIssue.Resolve("resolve");
			Assert.Equal(IssueStatus.Resolved, dataQualityIssue.Status);
			Assert.NotNull(dataQualityIssue.ResolvedAtUtc);
			Assert.Equal("resolve", dataQualityIssue.ResolutionNotes);
		}

		[Fact]
		public void Resolve_WhenIssueIsNotOpen_ShouldThrow()
		{
			Guid entityId = Guid.NewGuid();
			DataQualityIssue dataQualityIssue = new DataQualityIssue(
				"Type 10", entityId, "ruleCode",
				"description", IssueSeverity.High);
			dataQualityIssue.Resolve("resolve");
			Action action = () => dataQualityIssue.Resolve("resolve");
			Assert.Throws<InvalidOperationException>(action);
		}

		[Fact]
		public void Resolve_WhenNotesAreEmpty_ShouldThrow()
		{
			Guid entityId = Guid.NewGuid();
			DataQualityIssue dataQualityIssue = new DataQualityIssue(
				"Type 10", entityId, "ruleCode",
				"description", IssueSeverity.High);
			Action action = () => dataQualityIssue.Resolve("  ");
			Assert.Throws<ArgumentException>(action);
		}
    }

}