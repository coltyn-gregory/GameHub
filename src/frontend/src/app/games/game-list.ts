import { Component, inject, signal, OnInit } from '@angular/core';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';

import { Game, Platform, Studio } from './game';
import { GameService } from './game-service';

@Component({
  selector: 'app-game-list',
  imports: [NgbAlertModule],
  templateUrl: './game-list.html',
  styleUrl: './game-list.scss'
})
export class GameList implements OnInit {
  private readonly gameService = inject(GameService);

  protected readonly games = signal<Game[]>([]);
  protected readonly platforms = signal<Platform[]>([]);
  protected readonly studios = signal<Studio[]>([]);
  protected readonly selectedPlatformId = signal('');
  protected readonly selectedStudioId = signal('');
  protected readonly loading = signal(true);
  protected readonly error = signal<string | null>(null);

  ngOnInit(): void {
    this.gameService.getGames().subscribe({
      next: (games) => {
        this.games.set(games);
        this.platforms.set(this.extractPlatforms(games));
        this.studios.set(this.extractStudios(games));
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Failed to load games.');
        this.loading.set(false);
      }
    });
  }

  protected onPlatformChange(platformId: string): void {
    this.selectedPlatformId.set(platformId);
    this.reload();
  }

  protected onStudioChange(studioId: string): void {
    this.selectedStudioId.set(studioId);
    this.reload();
  }

  private reload(): void {
    this.loading.set(true);
    this.error.set(null);

    this.gameService
      .getGames(this.selectedPlatformId() || undefined, this.selectedStudioId() || undefined)
      .subscribe({
        next: (games) => {
          this.games.set(games);
          this.loading.set(false);
        },
        error: () => {
          this.error.set('Failed to load games.');
          this.loading.set(false);
        }
      });
  }

  private extractPlatforms(games: Game[]): Platform[] {
    const byId = new Map<string, Platform>();
    for (const game of games) {
      for (const platform of game.platforms) {
        byId.set(platform.id, platform);
      }
    }
    return [...byId.values()].sort((a, b) => a.name.localeCompare(b.name));
  }

  private extractStudios(games: Game[]): Studio[] {
    const byId = new Map<string, Studio>();
    for (const game of games) {
      byId.set(game.studio.id, game.studio);
    }
    return [...byId.values()].sort((a, b) => a.name.localeCompare(b.name));
  }
}
