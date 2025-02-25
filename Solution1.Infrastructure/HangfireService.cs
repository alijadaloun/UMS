using System.Linq.Expressions;
using Hangfire;
namespace Solution1.Infrastructure;
public class HangfireService
{
    public void ScheduleDaily<T>(Expression<Action<T>> expression )
    {
        RecurringJob.AddOrUpdate(expression,Cron.Daily);
        
    }

    public void ScheduleHourly<T>( Expression<Action<T>> expression )
    {
        RecurringJob.AddOrUpdate(expression,Cron.Hourly);
        
    }

}