export interface AlbumModel {
  id: number;

  title: string;
  releaseDate: Date;

  artistId: number;
  genreId: number;
  artistName: string;
  genreName: string;
}
