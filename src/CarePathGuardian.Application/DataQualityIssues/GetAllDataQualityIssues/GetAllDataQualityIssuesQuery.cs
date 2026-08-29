using CarePathGuardian.Domain.DataQualityIssues;

namespace CarePathGuardian.Application.DataQualityIssues.GetAllDataQualityIssues;
public sealed record GetAllDataQualityIssuesQuery(IssueStatus? Status, IssueSeverity? Severity, int Page = 1, int PageSize = 20, string? sort = null);