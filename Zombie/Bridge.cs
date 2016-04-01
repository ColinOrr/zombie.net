using EdgeJs;
using System;
using System.Dynamic;
using System.Linq;
using Zombie.Properties;

namespace Zombie
{
    /// <summary>
    /// Delegates properties and method calls to the JavaScript bridge (see Bridge.js) using the Edge.js callback pattern. 
    /// </summary>
    public class Bridge : DynamicObject
    {
        private readonly dynamic target;

        /// <summary>
        /// Constructs a new bridge to an object returned by the JavaScript.
        /// </summary>
        /// <param name="script">JavaScript that creates and returns a bridge object.</param>
        public Bridge(string script)
        {
            this.target = Edge.Func(Resources.Bridge + script)(null).Result;
        }

        /// <summary>
        /// Constructs a new bridge to the specified JavaScript target.
        /// </summary>
        /// <param name="target">The JavaScript bridge object that has methods to get and set properties, invoke methods and evaluate scripts.</param>
        public Bridge(dynamic target)
        {
            this.target = target;
        }

        /// <summary>
        /// Evaluates JavaScript directly 'this' is bound to the target object.
        /// </summary>
        public dynamic @exec(string script)
        {
            try
            {
                var result = target.eval(script).Result;
                if (result is ExpandoObject) result = new Bridge(result);
                return result;
            }
            catch (AggregateException e) { throw e.InnerException; }
        }

        /// <summary>
        /// Evaluates JavaScript directly within the context of the target object.
        /// Particularly useful for expressing lambdas, but can be used to execute any JavaScript that cannot be expressed in C#.
        /// </summary>
        /// <example>
        /// var visibleText = elements("filter(e => e.visible).map(e => e.textContent)");
        /// </example>
        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            try
            {
                var script = "this." + args.Single().ToString();
                result = target.eval(script).Result;
                if (result is ExpandoObject) result = new Bridge(result);
                return true;
            }
            catch (AggregateException e) { throw e.InnerException; }
        }

        /// <summary>
        /// Gets a field from the target object.
        /// </summary>
        /// <remarks>Field names can be prefixed with @ to avoid clashes with C# keywords (e.g. var isChecked = input.@checked).</remarks>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            try
            {
                result = target.get(binder.Name.TrimStart('@')).Result;
                if (result is ExpandoObject) result = new Bridge(result);
                return true;
            }
            catch (AggregateException e) { throw e.InnerException; }
        }

        /// <summary>
        /// Sets a field from the target object.
        /// </summary>
        /// <remarks>Field names can be prefixed with @ to avoid clashes with C# keywords (e.g. input.@checked = true).</remarks>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            try
            {
                target.set(new { key = binder.Name.TrimStart('@'), value = value }).Wait();
                return true;
            }
            catch (AggregateException e) { throw e.InnerException; }
        }

        /// <summary>
        /// Gets the value at the specified index on the target object.
        /// </summary>
        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            try
            {
                result = target.get(indexes.Single()).Result;
                if (result is ExpandoObject) result = new Bridge(result);
                return true;
            }
            catch (AggregateException e) { throw e.InnerException; }
        }

        /// <summary>
        /// Invokes the function on the target object and returns the results.
        /// </summary>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                result = target.call(new { key = binder.Name, args = args }).Result;
                if (result is ExpandoObject) result = new Bridge(result);
                return true;
            }
            catch (AggregateException e) { throw e.InnerException; }
        }
    }
}
