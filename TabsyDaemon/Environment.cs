namespace TabsyDaemon
{
    public class Environment
    {
        public static string Version { get; private set; } = "0.1";
        public static string DockerUnixSocket { get; private set; } = "unix:///var/run/docker.sock";
    }
}
