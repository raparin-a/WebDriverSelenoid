using System;
using OpenQA.Selenium;
using Polly;
using Polly.Retry;
using SeleniumWebDriver.Configuration;
using SeleniumWebDriver.Exсeptions;
using static Polly.Policy;
using NotFoundException = SeleniumWebDriver.Exсeptions.NotFoundException;

namespace SeleniumWebDriver.Helpers
{
    internal static class WaitHelper
    {
        private static readonly int TotalTimeToWait = Settings.WebDriverOptions.Value.TimeoutOptions.TotalTimeToWaiteInMilliseconds;
        private static readonly int TimeBetweenRetries = Settings.WebDriverOptions.Value.TimeoutOptions.TimeBetweenRetryInMilliseconds;
        
        internal static T DoRetryWithReturn<T>(Func<T> func, int retries = 0)
        {
            var waiteAndRetryPolicy = HandleExceptionPolicy(GetRetries(retries), TimeBetweenRetries)
                .ExecuteAndCapture(func);
            
            if (waiteAndRetryPolicy.FinalException == null) return waiteAndRetryPolicy.Result;

            ThrowDriverException(waiteAndRetryPolicy.FinalException);
            ThrowWrappedNotFoundException(waiteAndRetryPolicy.FinalException);
            ThrowWrappedStaleElement(waiteAndRetryPolicy.FinalException);
            ThrowWrappedInvalidOperationException(waiteAndRetryPolicy.FinalException);

            throw new SeleniumWebDriverException(waiteAndRetryPolicy.FinalException.Message);
        }
        
        internal static T WaitForCondition<T>(Func<T> func, Func<T, bool> condition, int retries = 0)
        {
            var executeAndCapture = HandleExceptionPolicy(condition, GetRetries(retries), TimeBetweenRetries)
                .ExecuteAndCapture(func);
            
            if (executeAndCapture.FinalException == null) return executeAndCapture.Result;

            ThrowDriverException(executeAndCapture.FinalException);
            ThrowWrappedNotFoundException(executeAndCapture.FinalException);
            ThrowWrappedStaleElement(executeAndCapture.FinalException);
            ThrowWrappedInvalidOperationException(executeAndCapture.FinalException);

            throw new SeleniumWebDriverException(executeAndCapture.FinalException.Message);
        }

        internal static void DoRetry(Action action, int retries = 0)
        {
            var waiteAndRetryPolicy = HandleExceptionPolicy(GetRetries(retries), TimeBetweenRetries)
                .ExecuteAndCapture(action);

            if (waiteAndRetryPolicy.FinalException == null) return;

            ThrowDriverException(waiteAndRetryPolicy.FinalException);
            ThrowWrappedNotFoundException(waiteAndRetryPolicy.FinalException);
            ThrowWrappedStaleElement(waiteAndRetryPolicy.FinalException);
            ThrowWrappedInvalidOperationException(waiteAndRetryPolicy.FinalException);

            throw new SeleniumWebDriverException(waiteAndRetryPolicy.FinalException.Message);
        }

        private static int GetRetries(int retries) => retries == 0 ? TotalTimeToWait / TimeBetweenRetries : retries;
        private static RetryPolicy HandleExceptionPolicy(int retries, int timeBetweenRetries) 
            => Handle<InvalidOperationException>()
                .Or<NotFoundException>()
                .Or<StaleElementReferenceException>()
                .Or<WebDriverException>()
                .WaitAndRetry(retries, i => TimeSpan.FromMilliseconds(timeBetweenRetries));
        
        private static RetryPolicy<T> HandleExceptionPolicy<T>(Func<T, bool> condition, int retries, int timeBetweenRetries) 
            => HandleResult(condition)
                .Or<InvalidOperationException>()
                .Or<NotFoundException>()
                .Or<StaleElementReferenceException>()
                .Or<WebDriverException>()
                .WaitAndRetry(retries, i => TimeSpan.FromMilliseconds(timeBetweenRetries));

        private static void ThrowWrappedNotFoundException(Exception ex)
        {
            if (ex.GetType() != typeof(OpenQA.Selenium.NotFoundException) && ex.GetType() != typeof(NoSuchElementException)) return;
            throw new NotFoundException(ex.Message);
        }
        
        private static void ThrowWrappedStaleElement(Exception ex)
        {
            if (ex.GetType() != typeof(StaleElementReferenceException) && ex.GetType() != typeof(InvalidElementStateException)) return;
            throw new NotFoundException(ex.Message);
        }
        
        private static void ThrowWrappedInvalidOperationException(Exception ex)
        {
            if (ex.GetType() != typeof(InvalidOperationException) && !ex.Message.Contains("is not clickable at point")) return;
            throw new NotIntractableException(ex.Message);
        }
        
        private static void ThrowDriverException(Exception ex)
        {
            if (ex.GetType() == typeof(WebDriverException) 
                && (ex.Message.Contains("invalid argument") || ex.Message.Contains("cannot parse capability") 
                    || ex.Message.Contains("unrecognized chrome option") || ex.Message.Contains("json: cannot unmarshal")
                    || ex.Message.Contains("Session timed out or not found")))
                    throw new SeleniumWebDriverException(ex.Message);
        }
    }
}