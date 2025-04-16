export interface Knife {
  id: number,
  title: string,
  category: string,
  description: string,
  image: string,
  images: string[],
  price: number,
  isOnSale: boolean,
  createdAt: Date,
  knifeInfo: KnifeInfo
}

export interface KnifeInfo {
  overallLength: number,
  bladeLength: number,
  buttThickness: number,
  weight: number,
  handleMaterial: string,
  country: string,
  manufacturer: string,
  steelGrade: string;

  [key: string]: any;
}

export interface KnifeBriefly {
  id: number,
  title: string,
  category: string,
  image: string,
  price: number,
  isOnSale: boolean,
}

export type CreateKnifeDto = Omit<Knife, 'id' | 'createdAt'>;