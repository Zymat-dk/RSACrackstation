from primeserver import generatePrimes

def main():
    try:
        size = int(input("Size: "))
    except:
        main()
    p, q = generatePrimes(size)[0]
    print(f"p: {p}\nq: {q}\nn: {p*q}")

if __name__ == "__main__":
    main()

