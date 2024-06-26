// // middleware.js

import { NextRequest } from "next/server";
import { NextResponse } from "next/server";
// import Cookies from "js-cookie";

export async function middleware(request: NextRequest) {
  if (request.nextUrl.pathname === "/") {
    // Redirect to /sign-in
    return NextResponse.redirect(new URL("/sign-in", request.url));
  }

  // Continue to the requested path
  return NextResponse.next();
}
