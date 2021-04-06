using ClickUpViewer.Domain.Repositories;

namespace ClickUpViewer.Infrastructure.WebApi
{
    public static class ClickUpApiFactory
    {
        public static IClickUpRepository Create(string token)
        {
            return new Api(token);
        }
    }
}