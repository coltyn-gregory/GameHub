import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Game, UpdateGameRequest } from './game';

@Injectable({ providedIn: 'root' })
export class GameService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = '/api/games';

  getGames(platformId?: string, studioId?: string): Observable<Game[]> {
    let params = new HttpParams();
    if (platformId) {
      params = params.set('platformId', platformId);
    }
    if (studioId) {
      params = params.set('studioId', studioId);
    }
    return this.http.get<Game[]>(this.baseUrl, { params });
  }

  getById(id: string): Observable<Game> {
    return this.http.get<Game>(`${this.baseUrl}/${id}`);
  }

  update(id: string, request: UpdateGameRequest): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, request);
  }
}
