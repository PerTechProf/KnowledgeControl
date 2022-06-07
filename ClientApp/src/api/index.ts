export const getCookie = (name: string): string =>
  Object.fromEntries(document.cookie
    .split('; ')
    .map(cookie => 
      cookie.split('=')
    )
  )[name];