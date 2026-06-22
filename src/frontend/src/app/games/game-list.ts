import { Component, inject, signal, OnInit } from '@angular/core';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';

import { Game, Platform } from './game';
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
  protected readonly selectedPlatformId = signal('');
  protected readonly loading = signal(true);
  protected readonly error = signal<string | null>(null);

  ngOnInit(): void {
    this.gameService.getAll().subscribe({
      next: (games) => {
        this.games.set(games);
        this.platforms.set(this.extractPlatforms(games));
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
    this.loading.set(true);
    this.error.set(null);

    const request$ = platformId
      ? this.gameService.getByPlatform(platformId)
      : this.gameService.getAll();

    request$.subscribe({
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
}
