export interface TokenResponse {
  accessToken: string,
  accessTokenExpirationTime: Date,
  refreshToken: string
}

export interface JwtPayload {
  role?: string | string[];
  [key: string]: any;
}