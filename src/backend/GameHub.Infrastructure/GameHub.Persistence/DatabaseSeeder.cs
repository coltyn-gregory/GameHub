using GameHub.Domain.Entities;
using GameHub.Domain.ValueObjects.Game;
using GameHub.Domain.ValueObjects.Platform;
using GameHub.Domain.ValueObjects.Studio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformName = GameHub.Domain.ValueObjects.Platform.Name;
using StudioName = GameHub.Domain.ValueObjects.Studio.Name;

namespace GameHub.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(this IServiceProvider services)
    {
        using IServiceScope scope = services.CreateScope();

        GameHubDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<GameHubDbContext>();

        if (await dbContext.Games.AnyAsync())
        {
            return;
        }

        string pcId = "46298fc8-c3e7-484c-8e44-824d10a14c3c";
        string xboxId = "217d1825-e3e3-4df5-91c1-75f604b6854b";
        string playstationId = "567243c1-10bc-4460-84e7-aa3cf3291a57";
        string switchId = "229aa5c7-77b9-4691-af27-a251f79530cf";
        string macId = "6b0b1cb0-0a2d-42c8-b3cb-90d4167276b1";
        string linuxId = "bc5c2d89-f44f-43a4-97ad-df45eb251451";
        string iosId = "829a825d-cddc-4ad9-9602-18953c193e1c";
        string androidId = "11242982-03a0-4b63-a35e-149755b7726d";
        string stadiaId = "f3f0e04e-5ca5-4316-a01b-391e1929b2f4";

        Platform[] platforms =
        [
            Platform.Create(new PlatformId(pcId), new PlatformName("PC"))!,
            Platform.Create(new PlatformId(xboxId), new PlatformName("Xbox"))!,
            Platform.Create(new PlatformId(playstationId), new PlatformName("PlayStation"))!,
            Platform.Create(new PlatformId(switchId), new PlatformName("Nintendo Switch"))!,
            Platform.Create(new PlatformId(macId), new PlatformName("macOS"))!,
            Platform.Create(new PlatformId(linuxId), new PlatformName("Linux"))!,
            Platform.Create(new PlatformId(iosId), new PlatformName("iOS"))!,
            Platform.Create(new PlatformId(androidId), new PlatformName("Android"))!,
            Platform.Create(new PlatformId(stadiaId), new PlatformName("Stadia"))!
        ];

        StudioId studioA = new("8fb92031-eba2-45b8-90fc-7c65f56a0578");
        StudioId studioB = new("67b2542a-6e6d-4fb7-95c4-41a6713273f8");
        StudioId studioC = new("4e88d57f-8686-4c93-84df-bfbec3f4f1a1");
        StudioId studioD = new("de6fc49e-7a65-4e0a-bd07-e8257d57dfc7");
        StudioId studioE = new("29576891-b561-40e2-bab4-95fdacb4fbe1");
        StudioId studioF = new("336910b7-9a01-44ad-af6b-ef7449eaa4b1");

        Studio[] studios =
        [
            Studio.Create(studioA, new StudioName("Aurora Interactive"))!,
            Studio.Create(studioB, new StudioName("Nimbus Games"))!,
            Studio.Create(studioC, new StudioName("Quasar Forge"))!,
            Studio.Create(studioD, new StudioName("Verdant Pixel"))!,
            Studio.Create(studioE, new StudioName("Obsidian Owl"))!,
            Studio.Create(studioF, new StudioName("Halcyon Works"))!
        ];

        Game[] games =
        [
            Game.Create(new GameId("fe7d3c10-2b3f-48cc-bc75-a189338b7805"), new Title("Stellar Drift"), studioA, [new PlatformId(pcId), new PlatformId(xboxId), new PlatformId(playstationId)])!,
            Game.Create(new GameId("1579a66c-2b00-4f25-a878-37b43246ec5a"), new Title("Iron Vanguard"), studioA, [new PlatformId(pcId), new PlatformId(xboxId), new PlatformId(playstationId)])!,
            Game.Create(new GameId("5f22194b-3314-4b4c-9775-0a758a1dbb24"), new Title("Hollow Tides"), studioA, [new PlatformId(pcId), new PlatformId(playstationId)])!,
            Game.Create(new GameId("dd156d3d-8edb-4cb4-9d49-771f0c6960a0"), new Title("Crimson Circuit"), studioB, [new PlatformId(pcId), new PlatformId(playstationId)])!,
            Game.Create(new GameId("9239a371-ef07-4511-be2a-6555cf3302a8"), new Title("Lumen Vale"), studioB, [new PlatformId(pcId)])!,
            Game.Create(new GameId("e8a41484-a891-4028-8af3-e4199917935b"), new Title("Echoes of Tomorrow"), studioB, [new PlatformId(pcId), new PlatformId(switchId), new PlatformId(playstationId)])!,
            Game.Create(new GameId("d3b874ae-640a-4cfb-9cb6-b72b66b6c05f"), new Title("Neon Requiem"), studioC, [new PlatformId(pcId), new PlatformId(macId), new PlatformId(linuxId)])!,
            Game.Create(new GameId("c85041a8-7100-4fe6-9039-41474963dfb0"), new Title("Frostbound Saga"), studioC, [new PlatformId(pcId), new PlatformId(xboxId), new PlatformId(switchId)])!,
            Game.Create(new GameId("d7fe5051-289f-4d28-9412-179d9bf98a53"), new Title("Sunken Cathedral"), studioC, [new PlatformId(playstationId), new PlatformId(switchId)])!,
            Game.Create(new GameId("bd83c879-88ae-4ed9-b63c-1c887c12d28b"), new Title("Whisper Protocol"), studioD, [new PlatformId(pcId), new PlatformId(iosId), new PlatformId(androidId)])!,
            Game.Create(new GameId("002a2a05-0008-421e-b088-f30796315f99"), new Title("Garden of Gears"), studioD, [new PlatformId(switchId), new PlatformId(iosId)])!,
            Game.Create(new GameId("028bc91b-3347-49e4-80e3-1cb3c348691e"), new Title("Voidrunner"), studioD, [new PlatformId(pcId), new PlatformId(xboxId), new PlatformId(playstationId), new PlatformId(stadiaId)])!,
            Game.Create(new GameId("999d9d02-edbb-40e3-8465-026d03873ef8"), new Title("Ashen Throne"), studioE, [new PlatformId(pcId), new PlatformId(playstationId)])!,
            Game.Create(new GameId("a17d4624-ed6c-44dc-91f2-e90d7506985a"), new Title("Tidal Lockdown"), studioE, [new PlatformId(macId), new PlatformId(linuxId), new PlatformId(pcId)])!,
            Game.Create(new GameId("bc2c74c1-c613-49d1-b918-c79f90940ecb"), new Title("Clockwork Heart"), studioF, [new PlatformId(pcId), new PlatformId(switchId), new PlatformId(xboxId), new PlatformId(playstationId)])!
        ];

        dbContext.Platforms.AddRange(platforms);
        dbContext.Studios.AddRange(studios);
        dbContext.Games.AddRange(games);

        await dbContext.SaveChangesAsync();
    }
}
