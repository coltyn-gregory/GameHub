import { Routes } from '@angular/router';

import { GameList } from './games/game-list';
import { GameEdit } from './games/game-edit';

export const routes: Routes = [
  { path: '', component: GameList },
  { path: 'games/:id/edit', component: GameEdit },
];
