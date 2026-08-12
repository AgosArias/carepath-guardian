namespace CarePathGuardian.Domain.DataQualityIssues;
public class DataQualityIssue
{
    public Guid Id {get; private set; }		
	public string EntityType {get; private set; }
	public Guid EntityId {get; private set; }
	public string RuleCode {get; private set; }
	public string Description {get; private set; }
	public IssueSeverity Severity {get; private set; }
	public IssueStatus Status {get; private set; }
	public DateTime DetectedAtUtc {get; private set; }
	public DateTime? ResolvedAtUtc {get; private set; }
	public string? ResolutionNotes {get; private set; }
	private DataQualityIssue()
	{
		EntityType = string.Empty;
		RuleCode = string.Empty;
		Description = string.Empty;
	}
	public DataQualityIssue(
		string entityType, 
		Guid entityId,
		string ruleCode, 
		string description,
		IssueSeverity severity)
	{
		if (string.IsNullOrWhiteSpace(entityType)) throw new ArgumentException("Entity type is required.", nameof(entityType));
		if (entityId == Guid.Empty) throw new ArgumentException("Entity id is required.", nameof(entityId));
		if (string.IsNullOrWhiteSpace(ruleCode)) throw new ArgumentException("Rule code is required.", nameof(ruleCode));
		if (string.IsNullOrWhiteSpace(description))	throw new ArgumentException("Description is required.", nameof(description));

		Id = Guid.NewGuid();
		EntityId = entityId;
		EntityType = entityType.Trim();
		RuleCode = ruleCode.Trim();
		Description = description.Trim();
		Severity = severity;
		Status = IssueStatus.Open;
		DetectedAtUtc = DateTime.UtcNow;
		ResolvedAtUtc = null;
		ResolutionNotes = null;
	}
}