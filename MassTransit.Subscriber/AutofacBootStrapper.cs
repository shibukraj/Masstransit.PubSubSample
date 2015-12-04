namespace MassTransit.Subscriber
{
    using Autofac;

    public static class AutofacBootStrapper
    {
        public static ContainerBuilder Builder { get; private set; }
        private static IContainer Container { get; set; }

        private static bool IsBuildComplete { get; set; }

        public static void Init()
        {
            Builder = new ContainerBuilder();
        }

        public static void SetUpAutofacContainer()
        {
            if (!IsBuildComplete)
            {
                Container = Builder.Build();
                IsBuildComplete = true;
            }
            else
            {
                Builder.Update(Container);
            }
        }

        public static T GetInstance<T>(string name = null)
        {
            return string.IsNullOrEmpty(name) ? Container.Resolve<T>() : Container.ResolveNamed<T>(name);
        }
    }
}