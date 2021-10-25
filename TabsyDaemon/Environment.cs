namespace TabsyDaemon
{
    public class Environment
    {
        public static string Version { get; private set; } = "0.1";
        public static string DockerConnectionUrl { get; private set; } = "unix:///var/run/docker.sock";
        public static string RpcBindIp { get; private set; } = "127.0.0.1";
        public static string RpcBindPort { get; private set; } = "9000";
        public static string Directory { get; private set; } = "/var/lib/tabsy";
        public static string ContainerDirectory { get; private set; } = Directory + "/container";
        public static string ImagesDirectory { get; private set; } = Directory + "/images";
        public static string ImageRepo { get; private set; } = "https://raw.githubusercontent.com/Endelon-Hosting/TabsyDaemon/master/ExampleRepo";
    }
}
