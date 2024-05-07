using Domain.Exceptions;

namespace Domain.ValueObjects.SubmittedTask;

public record ReportId
{
    public ReportId(Guid value)
    {

        if (value == Guid.Empty)
        {
            throw new InvalidReportIdException("ReportId cannot be empty");
        }
        
        Value = value;
    }

    public Guid Value { get; }
    
    public static implicit operator Guid(ReportId reportId) => reportId.Value;
    public static implicit operator ReportId(Guid reportId) => new(reportId);
    
    public override string ToString() => Value.ToString();
}