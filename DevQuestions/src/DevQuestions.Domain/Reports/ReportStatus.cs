namespace DevQuestions.Domain.Reports;

public enum ReportStatus
{
    /// <summary>
    /// Статус открыт.
    /// </summary>
    Open,

    /// <summary>
    /// Статус в работе.
    /// </summary>
    InProgress,

    /// <summary>
    /// Статус решен.
    /// </summary>
    Resolved,

    /// <summary>
    /// Статус закрыт.
    /// </summary>
    Rejected
}