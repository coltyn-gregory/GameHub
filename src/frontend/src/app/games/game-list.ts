import { Component, inject, signal, OnInit } from '@angular/core';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';

import { Game } from './game';
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
  protected readonly loading = signal(true);
  protected readonly error = signal<string | null>(null);

  ngOnInit(): void {
    this.gameService.getAll().subscribe({
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
}
