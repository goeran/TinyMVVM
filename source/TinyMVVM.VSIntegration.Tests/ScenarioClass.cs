using System;

namespace TinyMVVM.VSIntegration.Tests
{
    public abstract class ScenarioClass<T> where T : class
    {
        protected T Given { get { return this as T; } }
        protected T And { get { return this as T; } }
        protected T When { get { return this as T; } }
        protected T Then { get { return this as T; } }
    }

}
