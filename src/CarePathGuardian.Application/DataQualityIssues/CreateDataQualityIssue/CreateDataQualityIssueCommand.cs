using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.Application.DataQualityIssues.CreateDataQualityIssue;

public sealed record CreateDataQualityIssueCommand(
	string EntityType,
    Guid EntityId,
    string RuleCode,
    string Description,
    IssueSeverity Severity
);