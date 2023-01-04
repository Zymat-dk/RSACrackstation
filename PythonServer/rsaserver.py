#!/usr/bin/env python3

from http.server import BaseHTTPRequestHandler, HTTPServer
from socketserver import ThreadingMixIn
from urllib.parse import parse_qs, urlparse
import json

from keygen import generatePrimes
from smalle import smallE

MAX_SIZE = 2048
DEFAULT_SIZE = 1024


class RequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        parsed_path = urlparse(self.path)
        params = parse_qs(parsed_path.query)
        params = parse_params(params)
        if "error" in params:
            self.send_error(400, params["error"])
            return

        response = self.create_response(params)
        print(response)
        self.send_header("Content-type", "application/json")
        self.end_headers()
        self.wfile.write(json.dumps(response).encode())  # Write the response to the client
        return

    def create_response(self, params: dict) -> dict:
        # Set default response
        response = {
            "method": self.command,
            "params": params,
            "request_version": self.request_version,
            "protocol_version": self.protocol_version,
        }
        match params["calculation_type"]:
            case "keygen":
                function_response = generatePrimes(params["size"])  # Get a response from the function
                response.update(function_response)  # Add function response to the response
                self.send_response(200)
            case "smalle":
                n, e, c = params["n"], params["e"], params["c"]
                function_response = smallE(n, e, c)  # Get a response from the function
                response.update(function_response)  # Add function response to the response
                self.send_response(200)
        return response


def parse_params(params: dict) -> dict:
    """
    Convert {size: ['4']} to {size: 4} and use DEFAULT_VAL as default case
    """
    params = {key: val[0] for key, val in params.items()}  # Flatten the dict
    calculation_type = params.get("calculation_type", "unknown")
    # Check if the calculation type is valid
    match calculation_type:
        case "keygen":
            # Handle keygen params
            params["calculation_type"] = "keygen"
            size = params.get("size", DEFAULT_SIZE)
            try:
                size = int(size)
            except ValueError:
                size = DEFAULT_SIZE
            if size < 2 or size > MAX_SIZE:
                size = DEFAULT_SIZE
            params["size"] = size
        case "smalle":
            # Handle smalle params
            params["calculation_type"] = "smalle"
            if not all(key in params for key in ("n", "e", "c")):
                params["error"] = "Missing parameters"
            for key in ("n", "e", "c"):
                try:
                    params[key] = int(params[key])
                except ValueError:
                    params["error"] = "Invalid parameter"
        case _:
            params["calculation_type"] = "unknown"
            params["error"] = "Invalid calculation type"

    return params


class ThreadedHTTPServer(ThreadingMixIn, HTTPServer):
    """Handle requests in a separate thread."""


if __name__ == "__main__":
    server = ThreadedHTTPServer(("localhost", 8080), RequestHandler)
    print("Starting server, use <Ctrl-C> to stop")
    server.serve_forever()
