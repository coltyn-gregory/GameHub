export interface Platform {
  id: string;
  name: string;
}

export interface Studio {
  id: string;
  name: string;
}

export interface Game {
  id: string;
  title: string;
  studio: Studio;
  platforms: Platform[];
}

export interface UpdateGameRequest {
  title: string;
  studioId: string;
  platformIds: string[];
}
