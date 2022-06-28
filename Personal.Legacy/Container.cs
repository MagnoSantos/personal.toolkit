namespace Personal.Legacy;
public class Container
{
    private static Container? _instance;
    public static Container Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Container();
            }

            return _instance;
        }
    }

    private readonly Dictionary<Type, Type> _types;
    private readonly List<Registration> _registrations;

    public Container()
    {
        _types = new Dictionary<Type, Type>();
        _registrations = new List<Registration>();
    }

    public void Register<TImplementation>()
    {
        Register<TImplementation, TImplementation>();
    }

    public void Register<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _types[typeof(TInterface)] = typeof(TImplementation);
    }

    public void Register<TInterface>(Func<Container, TInterface> registration) where TInterface : class
    {
        _registrations.Add(new Registration
        {
            RegistrationType = typeof(TInterface),
            Resolver = registration
        });
    }

    public TImplementation Resolve<TImplementation>()
    {
        return (TImplementation)Resolve(typeof(TImplementation));
    }

    private object Resolve(Type type)
    {
        if (_registrations.Any(r => type.IsAssignableFrom(r.RegistrationType)))
        {
            return ResolveFromRegistrations(type);
        }

        if (_types.ContainsKey(type))
        {
            return ResolveAutomatically(type);
        }

        throw new Exception($"Cannot resolve {type.FullName}.");
    }

    private object ResolveAutomatically(Type type)
    {
        var implementation = _types[type];
        var constructor = implementation.GetConstructors().First();
        var constructorParameters = constructor.GetParameters();
        if (constructorParameters.Length == 0)
        {
            return Activator.CreateInstance(implementation);
        }

        var parameters = constructorParameters.Select(parameter => Resolve(parameter.ParameterType)).ToArray();

        return constructor.Invoke(parameters);
    }

    private object ResolveFromRegistrations(Type type)
    {
        return _registrations
            .First(r => type.IsAssignableFrom(r.RegistrationType))
            .Resolver(this);
    }

    private class Registration
    {
        public Type RegistrationType { get; set; }
        public Func<Container, object> Resolver { get; set; }
    }
}
