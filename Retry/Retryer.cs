using System;
using System.Threading.Tasks;

namespace Retry
{
    public static class Retryer
    {
        public static async Task<T> RetryAsync<T>(Func<Task<T>> action, TimeSpan delay, int retryCount)
        {
            for (var i = 0; i < retryCount + 1; i++)
            {
                try
                {
                    return await action();
                }
                catch (Exception)
                {
                    if (i >= retryCount)
                        throw;

                    await Task.Delay(delay);
                }
            }

            return default;
        }
    }
}
