import { Category } from "./category.interface";

export interface Knife {
  id: number,
  title: string,
  categories: Category[],
  description: string | null,
  image: string | null,
  images: string[] | null,
  price: number,
  isOnSale: boolean,
  createdAt: Date,
  knifeInfo: KnifeInfo,

  isFavorite: boolean
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
  categories: Category[],
  image: string | null,
  price: number,
  isOnSale: boolean,

  isFavorite: boolean
}

export interface KnifesPaginated {
  knifes: KnifeBriefly[],
  totalCount: number
}

export interface CreateKnifeDto {
  title: string,
  categoryIds: number[],
  description: string | null,
  price: number,
  isOnSale: boolean,
  knifeInfo: KnifeInfo
}

export interface EditKnifeDto {
  id: number,
  title: string,
  categoryIds: number[],
  description: string | null,
  price: number,
  isOnSale: boolean,
  knifeInfo: KnifeInfo
}

export interface GetKnifesPaginationDto {
  search: string | null;
  sortItem: string | null;
  sortOrder: 'asc' | 'desc' | null;
  page: number | null;
  pageSize: number | null;
  categoryId: number | null,
}