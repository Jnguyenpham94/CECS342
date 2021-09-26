#include <stdlib.h>

struct Block {
    int block_size; // # of bytes in the data section
    struct Block* next_block; // in C, you have to use “struct Block” as the type
};

const int OVERHEAD_SIZE = sizeof(struct Block);
const int POINTER_SIZE = sizeof(void*);

// This pointer always points to the first block in the heap
// which is NOT in use. Any block linked from there must also
// NOT be in use.
struct Block* free_head;

void my_initialize_heap(int size) {
    free_head = (struct Block*)malloc(size);

    // The -> operator accesses the field of an object that we have
    // a pointer to.
    free_head->next_block = NULL;
    free_head->block_size = size;
}

void* my_alloc(int size) {
    // if size is not a multiple of POINTER_SIZE, you
    // must round it *UP* to the next multiple.
    if (size % POINTER_SIZE != 0)
    {
        //round to nearest positive multiple formula
        size = size + POINTER_SIZE - (size % POINTER_SIZE); 
    }


    // From now on, we will assume that "size" has been adjusted
    // as per above.

    // Next, walk through the nodes that are linked together starting
    // at free_head. Look for a node whose whose block_size is sufficient
    // to fit this request size.

    // 1. Declare a temporary pointer to use for the "walking". A second
    // pointer will point to the block we choose in the end. AKA best fitting block
    struct Block* temp;
    struct Block* chosen_block = NULL;

    // 2. Start the pointer at free_head.
    temp = free_head;
    //int diff = 0;
    // 3. Perform the "walk through a linked list" logic that you
    // learned in 274.
    while (temp != NULL) { // keep going until we walk off the end of the list.
        if (temp->block_size >= size) {
            // This block is big enough!
            // If we were doing FIRST FIT, we would definitely break here.
            // But this project uses BEST FIT, so you might NOT want
            // to break on the next line. You may need to make a copy
            // of temp to remember this block if it's the best fit that
            // you've found so far!
            // this is FIRST FIT!
            if (temp->block_size >= size && (chosen_block==NULL || temp->block_size < chosen_block->block_size)) {//adjust chosen_block to next smallest size possible block
                chosen_block = temp;
                //diff = chosen_block->block_size - size;
                if (chosen_block->block_size == size)//exactly equals case
                {
                    break;
                }
            }
            temp = temp->next_block;//walk temp forward
        }

    }

    // 4. If we get here and chosen_block is NULL, that means we could not
    // find a block. A real heap allocator would throw an exception;
    // we will just return NULL;
    if (chosen_block == NULL) {
        return NULL;
    }

    // Otherwise, we must decide:
    // a) Do we split the block? yes/no
    // b) Is this block the head node? yes/no

    // The four possible answers to a) and b) will give you four branches
    // to code.
    //TODO: if statemetents
    // Branch 1: we are not splitting the head node.
    if (chosen_block->block_size >= size) {
        struct Block* new_block = (struct Block*)((char*)chosen_block->block_size + size);
        new_block->block_size = size + OVERHEAD_SIZE + POINTER_SIZE;
        new_block->next_block = free_head->next_block;
        free_head->block_size = free_head->block_size - sizeof(new_block);
        free_head->next_block = NULL;
    }
    // Branch 2: we are splitting the head node.
    else if (chosen_block->block_size >= size && chosen_block->block_size - size >= OVERHEAD_SIZE+ POINTER_SIZE) {
        struct Block* new_block = (struct Block*)((char*)chosen_block->block_size + size);
        new_block->block_size = size + OVERHEAD_SIZE + POINTER_SIZE;
        new_block->next_block = free_head->next_block;
        free_head->block_size = free_head->block_size - sizeof(new_block);
        free_head->next_block = new_block->next_block->next_block;
    }
    // Branch 3: we are not splitting an interior node.
    else if (chosen_block == free_head) {
        struct Block* new_block = (struct Block*)((char*)chosen_block->block_size + size);
        new_block->block_size = size + OVERHEAD_SIZE + POINTER_SIZE;
        new_block->next_block = free_head->next_block;
        free_head->block_size = free_head->block_size - sizeof(new_block);
        free_head->next_block = new_block->next_block->next_block;
    }
    // Branch 4: we are splitting an interior node.
    else {
        struct Block* new_block = (struct Block*)((char*)chosen_block->block_size + size);
        new_block->block_size = size + OVERHEAD_SIZE + POINTER_SIZE;
        new_block->next_block = free_head->next_block;
        free_head->block_size = free_head->block_size - sizeof(new_block);
        free_head->next_block = new_block->next_block->next_block;
    }

    return chosen_block->block_size + sizeof(OVERHEAD_SIZE);
    // To reassign chosen_block's next_block pointer, just give it a new value.
    // FOR EXAMPLE, to make chosen_block point AROUND the block that follows 
    // (you don't necessarily actually want to do this, just an example)
    // we would write
    // chosen_block->next_block = chosen_block->next_block->next_block
    // just like you did in 274.

    // If you need to "create" a new block, perhaps to split chosen_block itself,
    // you will simply create a new struct Block* variable and point it
    // to an address of where you want the new block object to be.
    // FOR EXAMPLE, if I wanted a new Block to exist exactly 100 bytes 
    // after where chosen_block currently is, I would write

    // struct Block *new_block = (struct Block*)((char*)chosen_block->nextBlock + 100);

    // and then initialize its fields:
    // new_block->size = ...;
    // new_block->next_block = ...;

    // This might mean that chosen_block is now smaller, and I might have
    // to change its size field...

    // See the PDF for the explanation for all the casting.
}

void my_free(void* data) {
    // TODO: initialize a local Block pointer by pointing it 
    // exactly "OVERHEAD_SIZE" bytes to the *left* of the data pointer.
    struct Block* rem_block = (struct Block*)((char*)data - OVERHEAD_SIZE);
    struct Block* temp = free_head;
    if (temp != NULL && temp->block_size == data) {
        temp = temp->next_block;
    }
    // Then return that block to the free list and change the
    // free_head variable as needed.
    rem_block->next_block = free_head;
}

void main() {
    // Test case 1 example
    // "1. Allocate an int; print the address of the returned pointer. 
    // Free the int, then allocate another int and print its address.
    // The addresses should be the same."
    printf("TEST 1\n");
    my_initialize_heap(100);
    int* a = my_alloc(sizeof(int)); // gimme an int.
    printf("a is at address %p\n", a); // %p prints the memory address of a pointer in hexadecimal.
    my_free(a);
    int* b = my_alloc(sizeof(int));
    printf("b is at address %p\n", b);
    // Make sure they're the same...
}