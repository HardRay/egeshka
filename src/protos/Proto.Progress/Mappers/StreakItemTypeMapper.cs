using System.ComponentModel;
using GrpcStreakItemType = Egeshka.Grpc.Progress.Enums.StreakItemType;
using ServiceStreakItemType = Egeshka.Core.Domain.Enums.StreakItemType;

namespace Egeshka.Proto.Progress.Mappers;

public static class StreakItemTypeMapper
{
    public static ServiceStreakItemType ToService(this GrpcStreakItemType grpcType)
    {
        return grpcType switch
        {
            GrpcStreakItemType.Progress => ServiceStreakItemType.Progress,
            GrpcStreakItemType.Freeze => ServiceStreakItemType.Freeze,
            _ => throw new InvalidEnumArgumentException(),
        };
    }

    public static GrpcStreakItemType ToProto(this ServiceStreakItemType grpcType)
    {
        return grpcType switch
        {
            ServiceStreakItemType.Progress => GrpcStreakItemType.Progress,
            ServiceStreakItemType.Freeze => GrpcStreakItemType.Freeze,
            _ => throw new InvalidEnumArgumentException(),
        };
    }
}
