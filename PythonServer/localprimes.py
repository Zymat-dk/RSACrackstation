from primeserver import generatePrimes

def main():
    try:
        size = int(input("Size: "))
    except:
        main()
    numbers, status, is_strong = generatePrimes(size)
    p, q = numbers
    print(f"{'Strong' if is_strong else 'Weak'} key\np: {p}\nq: {q}\nn: {p*q}")

if __name__ == "__main__":
    main()

