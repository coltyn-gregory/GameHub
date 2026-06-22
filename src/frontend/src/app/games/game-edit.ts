import { Component, inject, signal, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { forkJoin } from 'rxjs';

import { Platform, Studio } from './game';
import { GameService } from './game-service';

@Component({
  selector: 'app-game-edit',
  imports: [FormsModule, RouterLink, NgbAlertModule],
  templateUrl: './game-edit.html',
  styleUrl: './game-list.scss'
})
export class GameEdit implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly gameService = inject(GameService);

  protected readonly title = signal('');
  protected readonly studioId = signal('');
  protected readonly selectedPlatformIds = signal<Set<string>>(new Set());
  protected readonly studios = signal<Studio[]>([]);
  protected readonly platforms = signal<Platform[]>([]);
  protected readonly loading = signal(true);
  protected readonly saving = signal(false);
  protected readonly error = signal<string | null>(null);

  private gameId = '';

  ngOnInit(): void {
    this.gameId = this.route.snapshot.paramMap.get('id') ?? '';

    forkJoin({
      game: this.gameService.getById(this.gameId),
      all: this.gameService.getGames()
    }).subscribe({
      next: ({ game, all }) => {
        this.title.set(game.title);
        this.studioId.set(game.studio.id);
        this.selectedPlatformIds.set(new Set(game.platforms.map((p) => p.id)));

        const studiosById = new Map<string, Studio>();
        const platformsById = new Map<string, Platform>();
        for (const g of all) {
          studiosById.set(g.studio.id, g.studio);
          for (const p of g.platforms) {
            platformsById.set(p.id, p);
          }
        }
        this.studios.set([...studiosById.values()].sort((a, b) => a.name.localeCompare(b.name)));
        this.platforms.set([...platformsById.values()].sort((a, b) => a.name.localeCompare(b.name)));
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Failed to load game.');
        this.loading.set(false);
      }
    });
  }

  protected isPlatformSelected(platformId: string): boolean {
    return this.selectedPlatformIds().has(platformId);
  }

  protected togglePlatform(platformId: string, checked: boolean): void {
    const next = new Set(this.selectedPlatformIds());
    if (checked) {
      next.add(platformId);
    } else {
      next.delete(platformId);
    }
    this.selectedPlatformIds.set(next);
  }

  protected save(): void {
    this.saving.set(true);
    this.error.set(null);

    this.gameService
      .update(this.gameId, {
        title: this.title(),
        studioId: this.studioId(),
        platformIds: [...this.selectedPlatformIds()]
      })
      .subscribe({
        next: () => this.router.navigate(['/']),
        error: () => {
          this.error.set('Failed to save changes.');
          this.saving.set(false);
        }
      });
  }
}
