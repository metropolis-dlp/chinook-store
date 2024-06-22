export interface TrackModel {
  id: number;
  albumId: number;

  number: number;
  name: string,
  composer: string,
  milliseconds: number,
  unitPrice: number;
  mediaTypeName: string;
}
