using System;

[AttributeUsage(AttributeTargets.Method)]
public class LoadableStaticMethod : Attribute
{
	public Type[] Dependencies { get; private set; }

	public LoadableStaticMethod(Type[] dependencies = null)
	{
		Dependencies = dependencies ?? Type.EmptyTypes;
	}
}
