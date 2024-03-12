using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum ApplicationStatusType
{
    [Display(Name = "Сохраненный")] Saved = 1,
    [Display(Name = "На рассмотрении Секретариата")] Dispatched = 2,
    [Display(Name = "Возвращенный")] Returned = 3,
    [Display(Name = "На экспертизе")] OnExamination = 4,
    [Display(Name = "На рассмотрении Комиссии")] OnConsideration = 5,
    [Display(Name = "Награжденный")] Awarded = 6,
    [Display(Name = "Отклоненный")] Rejected = 7,
    [Display(Name = "Отложенный")] Postponed = 8,
    [Display(Name = "Исключенный")] Expelled = 9,
    [Display(Name = "На рассмотрении субъекта представления")] Incoming = 10,
}