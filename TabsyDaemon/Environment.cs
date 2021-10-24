namespace TabsyDaemon
{
    public class Environment
    {
        public static string Version { get; private set; } = "0.1";
        public static string DockerConnectionUrl { get; private set; } = "unix:///var/run/docker.sock";
        public static string RpcBindIp { get; private set; } = "127.0.0.1";
        public static string RpcBindPort { get; private set; } = "9000";
        public static string TabsyDirectory { get; private set; } = "/var/lib/tabsy";
        public static string TabsyContainerDirectory { get; private set; } = TabsyDirectory + "/container";
        public static string TabsyImagesDirectory { get; private set; } = TabsyDirectory + "/images";
    }
}
