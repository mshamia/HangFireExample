# What is Hangfire?
Hangfire is an open-sourced library that enables the developers to schedule events in the background with the utmost ease. It is a highly flexible library offering various features needed to make the job scheduling task a cake-walk. Hangfire in ASP.NET Core is the one library that you can’t miss out on.
 

# Fire-and-Forget Jobs
Fire-and-forget jobs are executed only once and almost immediately after creation.
````c#
var jobId = BackgroundJob.Enqueue(
    () => Console.WriteLine("Fire-and-forget!"));
````
# Delayed Jobs
Delayed jobs are executed only once too, but not immediately, after a certain time interval.
````c#
var jobId = BackgroundJob.Schedule(
    () => Console.WriteLine("Delayed!"),
    TimeSpan.FromDays(7));
````
# Recurring Jobs
Recurring jobs fire many times on the specified CRON schedule.
````c#
RecurringJob.AddOrUpdate(
    "myrecurringjob",
    () => Console.WriteLine("Recurring!"),
    Cron.Daily);
 ````
# Continuations
Continuations are executed when its parent job has been finished.
````c#
BackgroundJob.ContinueJobWith(
    jobId,
    () => Console.WriteLine("Continuation!"));
````
# Batches Pro
Batch is a group of background jobs that is created atomically and considered as a single entity.
````c#
var batchId = BatchJob.StartNew(x =>
{
    x.Enqueue(() => Console.WriteLine("Job 1"));
    x.Enqueue(() => Console.WriteLine("Job 2"));
});
````
# Batch Continuations Pro
Batch continuation is fired when all background jobs in a parent batch finished.
````c#
BatchJob.ContinueBatchWith(batchId, x =>
{
    x.Enqueue(() => Console.WriteLine("Last Job"));
});
````
 

#Hangfire Database Schema
When you start your ASP.NET Core Application for the time, Hangfire checks if you have an associated Hangfire Schema available in your database. If not, It will create a bunch of tables for you. Here is how your database would look like.
![image](https://user-images.githubusercontent.com/23368803/224045125-10fa83f5-de89-453b-a88c-20d015057653.png)

 
# Hangfire in ASP.NET Core
From the dashboard you will be able to monitor the jobs and their statuses. It also allows you to manually trigger available jobs. This is the ONE feature that sets Hangfire apart from other Schedulers. Built-in dashboard. How cool is that? The above screenshot is that of the Dashboard overview. Let’s explore the other tabs as well.

 
# Installations 

Install the Hangfire package in your .NET project using NuGet.
Add Hangfire services to your ASP.NET Core application using Startup.cs:
````c#
public void ConfigureServices(IServiceCollection services)
{
   services.AddHangfire(configuration => configuration
      .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
      .UseSimpleAssemblyNameTypeSerializer()
      .UseDefaultTypeSerializer()
      .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));
}
````
Add Hangfire middleware to your ASP.NET Core application using Startup.cs:
````c#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
   app.UseHangfireDashboard();
}
````
Create a background job:
````c#
BackgroundJob.Enqueue(() => Console.WriteLine("Hello, Hangfire!"));
````
Start the Hangfire background process:
````c#
app.UseHangfireServer();
````

# Disable Concurrent Execution

For more information about Sql Configrution and Options [hangfire.io](https://docs.hangfire.io/en/latest/configuration/using-sql-server.html).

````c#
// Add Hangfire SqlServerStorageOptions.
    services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));
````
