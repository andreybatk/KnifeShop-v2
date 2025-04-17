export interface Knife {
  id: number,
  title: string,
  category: string,
  description: string | null,
  image: string | null,
  images: string[] | null,
  price: number,
  isOnSale: boolean,
  createdAt: Date,
  knifeInfo: KnifeInfo
}

export interface KnifeInfo {
  overallLength: number | null,
  bladeLength: number | null,
  buttThickness: number | null,
  weight: number | null,
  handleMaterial: string | null,
  country: string | null,
  manufacturer: string | null,
  steelGrade: string | null;

  [key: string]: any;
}

export interface KnifeBriefly {
  id: number,
  title: string,
  category: string,
  image: string | null,
  price: number,
  isOnSale: boolean,
}

export interface CreateKnifeDto {
  title: string,
  category: string,
  description: string | null,
  price: number,
  isOnSale: boolean,
  knifeInfo: KnifeInfo
}