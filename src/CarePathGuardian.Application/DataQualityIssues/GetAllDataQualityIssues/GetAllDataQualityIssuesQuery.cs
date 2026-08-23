using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.Application.DataQualityIssues.GetAllDataQualityIssues;
public sealed record GetAllDataQualityIssuesQuery(IssueStatus? Status, IssueSeverity? Severity);