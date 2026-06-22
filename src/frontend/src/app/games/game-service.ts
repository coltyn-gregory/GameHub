import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Game } from './game';

@Injectable({ providedIn: 'root' })
export class GameService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = '/api/games';

  getAll(): Observable<Game[]> {
    return this.http.get<Game[]>(this.baseUrl);
  }
}
