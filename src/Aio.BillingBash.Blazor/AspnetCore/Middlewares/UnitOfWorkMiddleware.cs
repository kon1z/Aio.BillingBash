using Aio.BillingBash.AspnetCore.Data;

namespace Aio.BillingBash.AspnetCore.Middlewares
{
    public class UnitOfWorkMiddleware : IMiddleware
    {
        private readonly UnitOfWorkManager _unitOfWorkManager;

        public UnitOfWorkMiddleware(UnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
	        if (!new[] {HttpMethod.Delete.Method, HttpMethod.Patch.Method, HttpMethod.Post.Method, HttpMethod.Put.Method}
		            .Contains(context.Request.Method))
	        {
		        await next(context);
	        }
	        else
	        {
		        var unitOfWork = await _unitOfWorkManager.BeginAsync();
		        await next(context);
		        await unitOfWork.CompleteAsync(context.RequestAborted);
	        }
        }
    }
}
