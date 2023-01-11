import gmpy2

MAX_I = 10000
MAX_E = 25


def smallE(n: int, e: int, c: int) -> dict:
    response = {}
    if e < 1:
        response["error"] = f"e is too small (min 1)"
        return response
    elif e > MAX_E:
        response["error"] = f"e is too large (max {MAX_E})"
        return response
    for i in range(MAX_I):
        m, is_true_root = gmpy2.iroot(i * n + c, e)
        if is_true_root:
            m = int(m)
            response["i"] = i
            response["m"] = m
            print(m)
            break
    else:
        response["error"] = f"Unable to find a root in {MAX_I} iterations"
        return response

    return response


def main():
    n = int(input("n: "))
    e = int(input("e: "))
    c = int(input("c: "))
    response = smallE(n, e, c)
    if response["success"]:
        m = bytearray.fromhex(format(response["m"], "x")).decode()
        print(f"i: {response['i']}\nm: {m}")


if __name__ == "__main__":
    main()
