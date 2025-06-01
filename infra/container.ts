export const vpc = new sst.aws.Vpc("ContainerVPC");

export const cluster = new sst.aws.Cluster("ContainerCluster", {
  vpc,
});

export const appService = new sst.aws.Service("ApiService", { 
    cluster,
    image: {
        context: './src',
    },
    dev: {
        command: 'dotnet watch --launch-profile https'
    },
    serviceRegistry: !$dev ? {
        port: 80
    } : undefined
});

export let api: sst.aws.ApiGatewayV2 | undefined; 

if (!$dev) {
    api = new sst.aws.ApiGatewayV2("ApiGateway", {
        vpc
    });

    api.routePrivate("$default", appService.nodes.cloudmapService.arn)
}
