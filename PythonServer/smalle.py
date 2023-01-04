import gmpy2

MAX_I = 10000
MAX_E = 10


def smallE(n: int, e: int, c: int) -> dict:
    response = {"success": False}
    if e > MAX_E:
        return response
    for i in range(MAX_I):
        m, is_true_root = gmpy2.iroot(i * n + c, e)
        if is_true_root:
            m = int(m)
            response["success"] = True
            response["i"] = i
            response["m"] = m
            break

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
