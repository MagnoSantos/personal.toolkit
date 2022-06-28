using Personal.Legacy;

Container.Instance.Register<IHelloWorldService>(c => new HelloWorldService());

var service = Container.Instance.Resolve<IHelloWorldService>();

service.HelloWorld();